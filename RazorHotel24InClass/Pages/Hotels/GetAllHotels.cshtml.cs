using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotel24InClass.Interfaces;
using RazorHotel24InClass.Models;

namespace RazorHotel24InClass.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        private IHotelService _hotelService;

        public List<Hotel> Hotels { get; set; }

        public GetAllHotelsModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public void OnGet()
        {
            Hotels = _hotelService.GetAllHotel();
        }
    }
}
