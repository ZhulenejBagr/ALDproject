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
        }

        public void OnGetGenerate()
        {
            GS.GenerateGrid();
        }
    }
}