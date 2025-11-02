using System;
using RapidFireLib.Lib.Core;

namespace Domain.Aggregates
{
    public class BaseModel : IModel
    {
        public DateTime DataCollectionDate { get; set; }
        public string DataCollectionTime { get; set; }
        public int UserId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string VerificationNote { get; set; }
    }

}
