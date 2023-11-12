using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewshoreAir.Business.Services;
using NewshoreAir.API.Models;


namespace NewshoreAir.API.Controllers
{

    /// <summary>
    /// Controlador para gestionar los viajes.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyService _journeyService;
        private readonly IMapper _mapper;

        public JourneyController(IJourneyService journeyService, IMapper mapper)
        {
            _journeyService = journeyService ?? throw new ArgumentNullException(nameof(journeyService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Obtiene todos los viajes posibles desde el origen hasta el destino con un número máximo de paradas.
        /// </summary>
        /// <param name="origin">La estación de origen.</param>
        /// <param name="destination">La estación de destino.</param>
        /// <param name="maxStops">El número máximo de paradas.</param>
        /// <param name="currency">La moneda en la que se deben calcular los precios.</param>
        /// <returns>Una lista de todos los viajes posibles.</returns>
        [HttpGet("GetJourneys")]
        public async Task<ActionResult<List<JourneyDTO>>> Get([FromQuery] string origin, [FromQuery] string destination, [FromQuery] int maxStops, [FromQuery] string currency)
        {
            try
            {
                var journeys = await _journeyService.FindRoutesWithStops(origin, destination, maxStops, currency);
                if (journeys == null)
                {
                    return NotFound("No journeys found.");
                }

                List<JourneyDTO> journeyDtos = _mapper.Map<List<JourneyDTO>>(journeys);
                return Ok(journeyDtos);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }
    }
}
