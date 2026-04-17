using EMG_MED1000_BACKEND.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class ModeleController : ControllerBase
{
    private readonly ModeleService _modeleService;

    //Création d'un constructeur afin d'initialiser notre objet _modeleService d'accès à notre classe ModeleService
    public ModeleController(ModeleService modeleService)
    {
        _modeleService = modeleService;
    }

    //A partir de notre objet _marqueService, on aura acces a nos fonctionnalités métiers
    [HttpGet("modeles/{marqueId}")]
    public async Task<IActionResult> GetModelesByMarque(int marqueId)
    {
        var modeles = await _modeleService.GetAllModelesAsync(marqueId);

        if (modeles == null || !modeles.Any())
        {
            return NotFound("Aucun modèle trouvé pour cette marque");
        }

        return Ok(modeles);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllModelesFromTable ()
    {
        var modeles = await _modeleService.GetAllModelesFromTable();
        return Ok(modeles);
    }

    [HttpPost]
    public async Task<IActionResult> CreateModele([FromBody] Modele modele)
    {
        if (modele == null || string.IsNullOrWhiteSpace(modele.NomModele))
        {
            return BadRequest("Nom du modèle requis");
        }

        //Création du modele via notre service en lui fournissant les différents paramètres
        var createdModele = await _modeleService.CreateModele(modele.NomModele, modele.AnneeModele, modele.MarqueId);

        return CreatedAtAction(nameof(GetModeleById), new { id = createdModele.ModeleId }, createdModele);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateModele(int id, [FromBody] Modele modele)
    {
        if (modele == null || id <=0)
            return BadRequest("Les données sont invalides");

        var success = await _modeleService.UpdateModele(id, modele.NomModele, modele.AnneeModele, modele.MarqueId);
        
        if (!success)
            return NotFound($"Aucun modele trouvé avec l'ID {id}");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModele(int id)
    {
        var success = await _modeleService.DeleteModele(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Méthode GET pour récupérer un modèle par son ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetModeleById(int id)
    {
        // Appel à la méthode du service pour récupérer le modèle
        var modele = await _modeleService.GetModeleByIdAsync(id);

        if (modele == null)
        {
            // Si le modèle n'est pas trouvé, retourner NotFound
            return NotFound($"Aucun modèle trouvé avec l'ID {id}");
        }

        return Ok(modele);
    }
}