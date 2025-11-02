using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RapidFireLib.Lib.Core;
using RapidFireLib.Providers;
using RapidFireLib.View.Models.Identity;

namespace Domain.Aggregates
{
    [Table("UserGeo")]
    public class UserGeo : UserGeoModel
    {
        [Layer(1, typeof(Division), "Division", "DivisionId", "DivisionName", "#ffe6dc", "fas fa-city")]
        public string DivisionId { get; set; }
        [Layer(2, typeof(District), "District", "DistrictId", "DistrictName", "#ddeeca", "fa-solid fa-tree-city", 1)]
        public string DistrictId { get; set; }
        [Layer(3, typeof(Upazila), "Upazila", "UpazilaId", "UpazilaName", "#ffefbe", "fa-solid fa-mountain-city", 1, 2)]
        public string UpazilaId { get; set; }
        [Layer(4, typeof(Unions), "Union", "UnionId", "UnionName", "#fce1eb", "fa-regular fa-building", 1, 2, 3)]
        public string UnionId { get; set; }
        [Layer(5, typeof(Village), "Village", "VillageId", "VillageName", "#d4edeb", "fa-solid fa-tree", 1, 2, 3, 4)]
        public string VillageId { get; set; }
    }

    public class Division : IModel
    {
        [Key]
        public string DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionNameBN { get; set; }
    }
    public class District : IModel
    {
        [Key]
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameBN { get; set; }
        public string DivisionId { get; set; }
    }
    public class Upazila : IModel
    {
        [Key]
        public string UpazilaId { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameBN { get; set; }
        public string DistrictId { get; set; }
        public string DivisionId { get; set; }
    }
    public class Unions : IModel
    {
        [Key]
        public string UnionId { get; set; }
        public string UnionName { get; set; }
        public string UnionNameBN { get; set; }
        public string UpazilaId { get; set; }
        public string DistrictId { get; set; }
        public string DivisionId { get; set; }
    }
    public class Village : IModel
    {
        [Key]
        public string VillageId { get; set; }
        public string VillageName { get; set; }
        public string VillageNameBN { get; set; }
        public string UnionId { get; set; }
        public string UpazilaId { get; set; }
        public string DistrictId { get; set; }
        public string DivisionId { get; set; }
    }

}
