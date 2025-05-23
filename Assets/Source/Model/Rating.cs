﻿using System;
using System.Collections.Generic;

namespace Source.Model
{
    public enum Rating
    {
        MaTsu,
        MasterTaipan,
        Taipan,
        Compradore,
        GalleyHand,
        Worse,
        Worst
    }

    public static class RatingExtensions
    {
        public static string LocalizedName(this Rating rating)
        {
            return rating switch
            {
                Rating.MaTsu => Strings.Rating_MaTsu,  
                Rating.MasterTaipan => Strings.Rating_MasterTaipan,
                Rating.Taipan => Strings.Rating_Taipan,
                Rating.Compradore => Strings.Rating_Compradore,
                Rating.GalleyHand => Strings.Rating_GalleyHand,
                Rating.Worse => "",
                Rating.Worst => "",
                _ => throw new ArgumentOutOfRangeException(nameof(rating), rating, null)
            };
        }

        public static Rating ForFinalCash(Money amount)
        {
            return amount >= 50000 ? Rating.MaTsu :
                amount >= 8000 ? Rating.MasterTaipan :
                amount >= 1000 ? Rating.Taipan :
                amount >= 500 ? Rating.Compradore :
                amount >= 100 ? Rating.GalleyHand :
                amount >= 0 ? Rating.Worse : Rating.Worst;
        }
    }
}