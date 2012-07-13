using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

namespace BaoHien.Services.MeasurementUnits
{
    public class MeasurementUnitService : BaseService<MeasurementUnit>
    {
        public MeasurementUnit GetMeasurementUnit(System.Int32 id)
        {
            MeasurementUnit measurementUnit = OnGetItem<MeasurementUnit>(id.ToString());

            return measurementUnit;
        }
        public List<MeasurementUnit> GetMeasurementUnits()
        {
            List<MeasurementUnit> measurementUnits = OnGetItems<MeasurementUnit>();

            return measurementUnits;
        }
        public bool AddMeasurementUnit(MeasurementUnit materialInStock)
        {
            return OnAddItem<MeasurementUnit>(materialInStock);
        }
        public bool DeleteMeasurementUnit(System.Int32 id)
        {
            return OnDeleteItem<MeasurementUnit>(id.ToString());
        }
        public bool UpdateMeasurementUnit(MeasurementUnit measurementUnit)
        {
            return OnUpdateItem<MeasurementUnit>(measurementUnit, measurementUnit.Id.ToString());
        }
    }
}
