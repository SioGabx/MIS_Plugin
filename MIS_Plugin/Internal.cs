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

namespace MIS_Plugin
{
    partial class Commands
    {
        public Document AcDoc
        {
            get { return AcAp.DocumentManager.MdiActiveDocument; }
        }
        public ObjectId[] GetSelectionSet()
        {
            Autodesk.AutoCAD.ApplicationServices.Document doc = AcAp.DocumentManager.MdiActiveDocument;
            //var db = doc.Database;
            var ed = doc.Editor;

            SelectionSet selection = ed.SelectImplied().Value;
            if (selection != null)
            {
                ObjectId[] objectIdCollection = selection.GetObjectIds();
                return objectIdCollection;
            }
            else
            {
                return new ObjectId[0];
            }
        }

        LayerTableRecord GetLayerFromStringName(string layer_name)
        {
            var doc = AcAp.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                LayerTable layer_datatable = tr.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                LayerTableRecord layer_datatablerecord = tr.GetObject(layer_datatable[layer_name], OpenMode.ForWrite) as LayerTableRecord;
                return layer_datatablerecord;
            }
        }


    }


    public static class Extensions
    {
        public static Entity GetEntity(this ObjectId objectId, Transaction tr)
        {
            Entity ent = (Entity)tr.GetObject(objectId, OpenMode.ForWrite);
                return ent;
        }
    }

}
