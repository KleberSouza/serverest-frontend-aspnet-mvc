using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using serverest_frontend_aspnet_mvc.Services;
using System.Security.Claims;

namespace serverest_frontend_aspnet_mvc.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ServeRestService _serveRestService;

        public UsuariosController(ServeRestService serveRestService)
        {
            _serveRestService = serveRestService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var response = await _serveRestService.LoginAsync(loginRequest.Email, loginRequest.Password);

                if (response != null)
                {
                    // Armazena o token na sessão para uso futuro
                    HttpContext.Session.SetString("AuthToken", response.Authorization);

                    var user = await _serveRestService.GetUserByEmailAsync(loginRequest.Email);
                    var perfil = user.administrador == "true" ? "Administrador" : "Cliente";


                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user._id),
                        new Claim(ClaimTypes.Name, user.nome), // Nome do usuário
                        new Claim(ClaimTypes.Role, perfil ) // Perfil do usuário
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    var propriedadesDeAutenticacao = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTime.Now.ToLocalTime().AddMinutes(10),
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(principal, propriedadesDeAutenticacao);

                    return RedirectToAction("Index","Home");

                }

                ViewData["Message"] = "Usuário e/ou Senha inválidos!";
                return View();
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Login failed: " + ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            // Remove o token da sessão
            HttpContext.Session.Remove("AuthToken");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var users = await _serveRestService.GetAllUsuarios();
            return View(users);
        }
    }
}
