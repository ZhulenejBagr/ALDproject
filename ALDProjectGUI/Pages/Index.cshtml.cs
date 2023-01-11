using ALDProjectGUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ALDProjectGUI.Pages
{
    public class IndexModel : PageModel
    {
        public GridService GS { get; private set; }

        public IndexModel(GridService gs)
        {
            GS = gs;
        }

        public void OnGet()
        {
            GS.EmptyGrid();
            //var seed = Request.Form["Seed"];
            return;
        }

        public void OnPost()
        {
            // vstupy jsou neošetřené, pro demonstraci generace nepodstatné
            var seed = Request.Form["Seed"];
            var xdim = Request.Form["Xdim"];
            var ydim = Request.Form["Ydim"];
            GS.GridSizeX = (int)long.Parse(xdim);
            GS.GridSizeY = (int)long.Parse(ydim);
            GS.GridSeed = (int)long.Parse(seed);
            GS.GenerateGrid();
            return;
        }

    }
}