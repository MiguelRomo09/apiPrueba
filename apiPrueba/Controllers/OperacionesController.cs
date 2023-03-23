using apiPrueba.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace apiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionesController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Double> Post(double num1, double num2, string operacion)
        {
            double resultado = 0;
            if (num1 == 0)
            {
                return BadRequest("No se ingreso el primer número");
            }
            if (num2 == 0)
            {
                return BadRequest("No se ingreso el segundo número");
            }
            if (operacion == null)
            {
                return BadRequest("No se ingreso la operacion");
            }


            if (operacion.ToLower() == "suma" || operacion == "+")
            {
                resultado =  num1 + num2;
            }
            else if (operacion.ToLower() == "resta" || operacion == "-")
            {
                resultado = num1 - num2;
            }
            else if (operacion.ToLower() == "multiplicacion" || operacion == "*")
            {
                resultado = num1 * num2;
            }
            else if (operacion.ToLower() == "division" || operacion == "/")
            {
                if (num2 == 0)
                {
                    return BadRequest("El segundo numero no puede ser 0 si se quiere hacer una divisíon");
                }
                resultado = num1 / num2;
            }
            else
            {
                return BadRequest("No se ingreso una operación valida");
            }

            return Ok(resultado);
        }


        [HttpGet]
        public ActionResult<Double> Get()
        {
         
            double num1 = 0;
            double num2 = 0;
            string operacion = "";
            
                num1 = Convert.ToDouble(Request.Headers["num1"]);
            
            
                num2 = Convert.ToDouble(Request.Headers["num2"]);
           
           
                operacion = Request.Headers["operacion"];

            double resultado = 0;
            if (num1 == 0)
            {
                return BadRequest("No se ingreso el primer número");
            }
            if (num2 == 0)
            {
                return BadRequest("No se ingreso el segundo número");
            }
            if (operacion == null)
            {
                return BadRequest("No se ingreso la operacion");
            }


            if (operacion.ToLower() == "suma" || operacion == "+")
            {
                resultado = num1 + num2;
            }
            else if (operacion.ToLower() == "resta" || operacion == "-")
            {
                resultado = num1 - num2;
            }
            else if (operacion.ToLower() == "multiplicacion" || operacion == "*")
            {
                resultado = num1 * num2;
            }
            else if (operacion.ToLower() == "division" || operacion == "/")
            {
                if (num2 == 0)
                {
                    BadRequest("El segundo número no puede ser 0 si se quiere hacer una divisíon");
                }
                resultado = num1 / num2;
            }
            else
            {
                return BadRequest("No se ingreso una operación valida");
            }

            return Ok(resultado);
        }
    }
}
