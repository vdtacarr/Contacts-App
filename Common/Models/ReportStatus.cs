using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public enum ReportStatus
    {
        [Display(Name = "Hazırlanıyor")]
        Preparing,

        [Display(Name = "Hazırlandı")]
        Prepared
    }
}
