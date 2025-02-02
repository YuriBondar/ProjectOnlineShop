using System.ComponentModel.DataAnnotations;

namespace ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterCustomerDTOs
{
    public class RegisterCustomerDTO
    {


        public string UserName { get; set; } = null!;

        public string Password { get; set; }



        [EmailAddress(ErrorMessage = "Ungültiger Email.")]
        public string CustomerEmail { get; set; } = null!;



        [Required(ErrorMessage = "Vorname ist erforderlich")]
        [RegularExpression(@"^[A-ZÖÄÜ][A-Za-zÄÖÜäöüß]*([- ][A-ZÖÄÜ][A-Za-zÄÖÜäöüß]+)*$",
           ErrorMessage = "Ein Vorname besteht aus einem oder mehreren Wörtern mit einem Großbuchstaben, " +
           "die durch ein Leerzeichen oder einen Bindestrich getrennt sind.")]
        public string CustomerFirstName { get; set; } = null!;



        [Required(ErrorMessage = "Nachname ist erforderlich")]
        [RegularExpression(@"^[A-ZÖÄÜ][A-Za-zÄÖÜäöüß]*([- ][A-ZÖÄÜ][A-Za-zÄÖÜäöüß]+)*$",
            ErrorMessage = "Ein Nachname besteht aus einem oder mehreren Wörtern mit einem Großbuchstaben, " +
            "die durch ein Leerzeichen oder einen Bindestrich getrennt sind.")]
        public string CustomerLastName { get; set; } = null!;



        [Required(ErrorMessage = "Straßenname ist erforderlich")]
        [RegularExpression(@"^[A-ZÖÄÜ][A-Za-zÄÖÜäöüß]*([- ][A-ZÖÄÜ][A-Za-zÄÖÜäöüß\.]+)*$",
            ErrorMessage = "Ein Straßenname besteht aus einem oder mehreren Wörtern mit einem Großbuchstaben, " +
            "die durch ein Leerzeichen oder einen Bindestrich getrennt sind. " +
            "Abkürzungen mit einem Punkt wie 'Göstinger ßtr.' sind möglich.")]
        public string CustomerStreet { get; set; } = null!;



        [Required(ErrorMessage = "Hausnummer ist erforderlich")]
        [RegularExpression(@"^[0-9][A-Za-zÄÖÜäöüß0-9\/]*$",
            ErrorMessage = "Die Hausnummer beginnt mit einer Ziffer. Sie kann aus Ziffern und Buchstaben bestehen.")]
        public string CustomerHausNumber { get; set; } = null!;



        [Required(ErrorMessage = "Postleitzahl ist erforderlich")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Die Postleitzahl muss genau aus 4 Ziffern bestehen.")]
        public string CustomerPostIndex { get; set; } = null!;



        [Required(ErrorMessage = "Stadt ist erforderlich")]
        [RegularExpression(@"^[A-ZÖÄÜ][A-Za-zÄÖÜäöüß\.]*([- ][A-ZÖÄÜ][A-Za-zÄÖÜäöüß\.]+)*$",
            ErrorMessage = "Ein Stadtnname besteht aus einem oder mehreren Wörtern mit einem Großbuchstaben, " +
            "die durch ein Leerzeichen oder einen Bindestrich getrennt sind. " +
            "Abkürzungen mit einem Punkt wie 'St. Pölten' sind möglich.")]
        public string CustomerCity { get; set; } = null!;




        [Required(ErrorMessage = "Telefonnummer ist erforderlich")]
        [Phone(ErrorMessage = "Ungültiger Telefonnummer.")]
        public string CustomerPhone { get; set; } = null!;
    }
}
