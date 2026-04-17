using EMG_MED1000_BACKEND.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class MarqueController : ControllerBase
{
    private readonly MarqueService _marqueService;

    //Création d'un constructeur afin d'initialiser notre objet _marqueService d'accès à notre classe MarqueService
    public MarqueController(MarqueService marqueService)
    {
        _marqueService = marqueService;
    }

    //A partir de notre objet _marqueService, on aura acces a nos fonctionnalités métiers

    [HttpGet]
    public async Task<IActionResult> GetAllMarques ()
    {
        var marque = await _marqueService.GetAllMarquesAsync();
        return Ok(marque);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMarque([FromBody] Marque marque)
    {
        if (marque == null || string.IsNullOrWhiteSpace(marque.NomMarque))
        {
            return BadRequest("Nom de la marque requis");
        }

        //Création de la marque via notre service en lui fournissant les différents paramètres
        var createdMarque = await _marqueService.CreateMarque(
            marque.NomMarque,
            marque.Modeles ?? new List<Modele>()
        );

        return CreatedAtAction(nameof(GetMarqueById), new { id = createdMarque.MarqueId }, createdMarque);
    }

    [HttpPut("{id}")]
    //Autorisation a Admin
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateMarque(int id, [FromBody] Marque marque)
    {
        if (marque == null || id <=0)
            return BadRequest("Les données sont invalides");

        var success = await _marqueService.UpdateMarque(
            id, 
            marque.NomMarque, 
            marque.Modeles ?? new List<Modele>()
            );
        
        if (!success)
            return NotFound($"Aucune marque trouvée avec l'ID {id}");

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMarque(int id)
    {
        var success = await _marqueService.DeleteMarque(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMarqueById(int id)
    {
        if (id <= 0)
            return BadRequest("L'ID est invalide");

        var marque = await _marqueService.GetMarqueById(id);

        if (marque == null)
            return NotFound($"Aucune marque trouvée avec l'ID {id}");

        return Ok(marque); 
    }

}