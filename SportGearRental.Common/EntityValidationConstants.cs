using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Common
{
    public static class EntityValidationConstants
    {
        public static class UserValidation
        {
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 32;
        }

        public static class SportGear
        {
            public const int GearNameMinLength = 3;
            public const int GearNameMaxLength = 50;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;

            public const decimal PricePerDayMin = 0.0m;
            public const decimal PricePerDayMax = 500.0m;

            public const int ImageUrlMaxLength = 2048;
        }

        public static class Category
        {
            public const int CategoryNameMinLength = 3;
            public const int CategoryNameMaxLength = 30;
        }

        public static class Brand
        {
            public const int BrandNameMinLength = 2;
            public const int BrandNameMaxLength = 30;
        }

        public static class Rental
        {
            public const decimal TotalPriceMin = 0.0m;
            public const decimal TotalPriceMax = 5000.0m;
        }

        public static class Review
        {
            public const int ContentMinLength = 5;
            public const int ContentMaxLength = 300;

            public const int RatingMin = 1;
            public const int RatingMax = 5;
        }

        public static class GearCondition
        {
            public const int GearConditionNameMinLength = 3;
            public const int GearConditionNameMaxLength = 20;

            public const int GearDescriptionMinLength = 10;
            public const int GearDescriptionMaxLength = 500;
        }
    }
}
