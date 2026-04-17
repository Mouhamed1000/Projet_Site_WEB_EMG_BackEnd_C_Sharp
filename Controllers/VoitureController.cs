using EMG_MED1000_BACKEND.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class VoitureController : ControllerBase
{
    private readonly VoitureService _voitureService;

    //Création d'un constructeur afin d'initialiser notre objet _voitureService d'accès à notre classe VoitureService
    public VoitureController(VoitureService voitureService)
    {
        _voitureService = voitureService;
    }

    //A partir de notre objet _voitureService, on aura acces a nos fonctionnalités métiers

    [HttpGet]
    public async Task<IActionResult> GetAllVoitures ()
    {
        var voitures = await _voitureService.GetAllVoituresAsync();
        return Ok(voitures);
    }

    [HttpPost("upload-and-create")]
    public async Task<IActionResult> UploadAndCreate([FromForm] IFormFile file, [FromForm] StatutVoiture statut,
                                                     [FromForm] string description, [FromForm] DateTime anneeVoiture, [FromForm] int marqueId)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Image obligatoire");
        }

        //Upload de l'image
        var imagePath = await _voitureService.UploadImageAsync(file);

        //Création de la voiture via notre service en lui fournissant les différents paramètres
        var createdVoiture = await _voitureService.CreateVoiture(statut, imagePath, description, anneeVoiture, marqueId);

        return StatusCode(201, createdVoiture);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVoiture(int id, [FromBody] Voiture voiture)
    {
        if (voiture == null || id <=0)
            return BadRequest("Les données sont invalides");

        var success = await _voitureService.UpdateVoiture(id, voiture.Statut, voiture.Photo, voiture.Description, voiture.AnneeVoiture, voiture.MarqueId);
        
        if (!success)
            return NotFound($"Aucune voiture trouvée avec l'ID {id}");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVoiture(int id)
    {
        var success = await _voitureService.DeleteVoiture(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVoitureById(int id)
    {
        var voiture = await _voitureService.GetVoitureByIdAsync(id);
    
        if (voiture == null)
        {
            return NotFound("Voiture non trouvée");
        }
    
        return Ok(voiture);
    }
}