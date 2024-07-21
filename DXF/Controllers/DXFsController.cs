using DXF.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using netDxf;
using netDxf.Entities;

namespace DXF.Controllers
{
    [ApiController]
    public class DXFsController : ControllerBase
    {
        [Route("[action]")]
        [HttpPost]
        public IActionResult Rectangle(DXF_Rectangle obj)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var dxf = new DxfDocument();
                dxf.Comments.Clear();

                Vector3 bottomLeft = new Vector3(0, 0, 0);
                Vector3 bottomRight = new Vector3(obj.Width, 0, 0);
                Vector3 topRight = new Vector3(obj.Width, obj.Height, 0);
                Vector3 topLeft = new Vector3(0, obj.Height, 0);

                // Create the lines for the rectangle
                Line line1 = new Line(bottomLeft, bottomRight);
                Line line2 = new Line(bottomRight, topRight);
                Line line3 = new Line(topRight, topLeft);
                Line line4 = new Line(topLeft, bottomLeft);

                // Add the lines to the DXF document
                dxf.Entities.Add(line1);
                dxf.Entities.Add(line2);
                dxf.Entities.Add(line3);
                dxf.Entities.Add(line4);

                dxf.Save(Path.Combine(obj.FolderPath, $"{obj.FileName}.dxf"));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
