using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotel24InClass.Interfaces;
using RazorHotel24InClass.Models;

namespace RazorHotel24InClass.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private IHotelService _hservice;

        public List<Hotel>  AllHotels { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IHotelService hotelService)
        {
            _logger = logger;
            _hservice = hotelService;
        }

        public void OnGet()
        {
            AllHotels = _hservice.GetAllHotel();
        }
    }
}
