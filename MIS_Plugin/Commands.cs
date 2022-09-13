using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;

[assembly: CommandClass(typeof(MIS_Plugin.Commands))]

namespace MIS_Plugin
{
    partial class Commands
    {
        [CommandMethod("FORCELAYERCOLORTOENTITY", CommandFlags.UsePickSet)]
        public void FORCELAYERCOLORTOENTITY()
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;
            //var db = doc.Database;
            var ed = doc.Editor;

            ObjectId[] selection = GetSelectionSet();
            int selection_count = selection.Count();
           

            if (selection_count == 0 || selection == null)
            {
                return;
            }

            using (var tr = doc.TransactionManager.StartTransaction())
            {
                foreach (ObjectId ObjId in selection)
                {
                    Entity ent = (Entity)ObjId.GetEntity(tr);
                    string ent_layer = ent.Layer;
                    LayerTableRecord layer_datatablerecord = GetLayerFromStringName(ent_layer);
                    Autodesk.AutoCAD.Colors.Color layer_color = layer_datatablerecord.Color;

                    ent.Color = layer_color;

                }
                tr.Commit();
            }
            ed.WriteMessage("Opération réussie : " + selection_count.ToString() + " entité(s) affectée(s)");
        }
    }
}
