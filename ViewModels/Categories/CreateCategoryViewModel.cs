using System.ComponentModel.DataAnnotations;

namespace BlogAspNet.ViewModels.Categories;

public class CreateCategoryViewModel
{
    [Required(ErrorMessage = "O nome e obrigatorio")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 40 caracteres")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O slug e obrigatorio")]
    public string Slug { get; set; }
}