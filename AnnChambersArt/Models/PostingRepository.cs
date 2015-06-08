using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnnChambersArt.Models
{
    public class PostingRepository : IPostingRepository
    {
        // this won't stay here
        string filePath = "../../Content/art/";
        bool IPostingRepository.Create(ItemPosting posting)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ItemPosting> IPostingRepository.ShopPostings()
        {
            //throw new NotImplementedException();
            return new List<ItemPosting>()
            {
                new ItemPosting(
                    filePath + "Pasquale.jpg",                     
                    "Pasquale", 
                    "Pasquale", 
                    "Pasquale", 
                    "An abstract painting . Acrylic paint on canvas.", 
                    "102 cm x 80 cm x 15 mm", 
                    "£300"),
                new ItemPosting(
                    filePath + "Tanyas waterlilies.jpg", 
                    "Tanya's Waterlilies (19)", 
                    "Tanya's Waterlilies (19)", 
                    "Tanya's Waterlilies (19)", 
                    "Print of an abstract acrylic painting. Can be framed.", 
                    "60 x 46 x 4cm", 
                    "£120 not framed"),
                new ItemPosting(
                    filePath + "Blue Cow.jpg", 
                    "Blue Cow", 
                    "Blue Cow", 
                    "Blue Cow", 
                    "Print of an abstract acrylic painting. Can be framed.", 
                    "60 x 46 x 4cm", 
                    "£120 not framed")
            };
        }
        IEnumerable<ItemPosting> IPostingRepository.ArchivePostings()
        {
            //throw new NotImplementedException();
            return new List<ItemPosting>()
            {
                new ItemPosting(
                    filePath + "Paprika.jpg",                     
                    "Paprika", 
                    "Paprika", 
                    "Paprika", 
                    "An abstract painting . Acrylic paint on a deep profile canvas.", 
                    "102 x 76 x 4cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Golden Storm.jpg", 
                    "Golden Storm", 
                    "Golden Storm", 
                    "Golden Storm", 
                    "Impressionist painting. Acrylic paint on canvas with a 15 mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Fauna 1.jpg", 
                    "Fauna 1", 
                    "Fauna 1", 
                    "Fauna 1", 
                    "An abstract painting. Acrylic paint on deep profile canvas.", 
                    "40 x 30 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Fauna 2.jpg", 
                    "Fauna 2", 
                    "Fauna 2", 
                    "Fauna 2", 
                    "An abstract painting. Acrylic paint on deep profile canvas.", 
                    "40 x 30 cm", 
                    "(no longer for sale)")
                    ,
                new ItemPosting(
                    filePath + "Water Lilies 14.jpg", 
                    "Water Lilies 14", 
                    "Water Lilies 14", 
                    "Water Lilies 14", 
                    "A large impressionist painting. Acrylic Paint on canvas with a 15 mm profile.", 
                    "127 x 102 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Flame Trees.jpg", 
                    "Flame Trees", 
                    "Flame Trees", 
                    "Flame Trees", 
                    "Acrylic paint on textured surface. Painted on canvas with 15mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Praxinus.jpg", 
                    "Praxinus", 
                    "Praxinus", 
                    "Praxinus", 
                    "Acrylic paint on canvas with 15mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Joanne's Poppies.jpg", 
                    "Joanne's Poppies", 
                    "Joanne's Poppies", 
                    "Joanne's Poppies", 
                    "Acrylic paint on canvas-covered board, profile 3cm.", 
                    "76 x 76 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Travaganza.jpg", 
                    "Travaganza", 
                    "Travaganza", 
                    "Travaganza", 
                    "Abstract painting, conveying joy and celebration. Acrylic paint on canvas with a 4cm profile.", 
                    "127 x 102 cm.", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Blue River.jpg", 
                    "Blue River", 
                    "Blue River", 
                    "Blue River", 
                    "Impression of a rainy evening beside the Thames. Oil paint on canvas with a 4cm profile.", 
                    "40 x 40 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Glorious Summer.jpg", 
                    "Glorious Summer", 
                    "Glorious Summer", 
                    "Glorious Summer", 
                    "View of a summer garden. Acrylic paint on canvas with a 4 cm profile.", 
                    "61 x 46 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Bunting.jpg", 
                    "Bunting", 
                    "Bunting", 
                    "Bunting", 
                    "Painting inspired by the Jubilee. Acrylic paint on canvas with a 15 mm profile.", 
                    "76 x 61 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Jubilee.jpg", 
                    "Jubilee Jesting", 
                    "Jubilee Jesting", 
                    "Jubilee Jesting", 
                    "Abstract painting. Acrylic paint on canvas with a 4 cm profile.", 
                    "102 x 76 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Colour of Summer.jpg", 
                    "Colour of Summer", 
                    "Colour of Summer", 
                    "Colour of Summer", 
                    "Impression of a summer sunset. Oil paint on canvas with a 4cm profile.", 
                    "40 x 40 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Oberon's Dream.jpg", 
                    "Oberon's Dream", 
                    "Oberon's Dream", 
                    "Oberon's Dream", 
                    "An impressionist painting in glorious colours. Acrylic paint on canvas with a 15 mm profile.", 
                    "", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "A Forest Dream.jpg", 
                    "A Forest Dream", 
                    "A Forest Dream", 
                    "A Forest Dream", 
                    "A large impressionist painting in glorious colours. Acrylic paint on canvas with a 15 mm profile.", 
                    "127 x 102 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Arboretum.jpg", 
                    "Arboretum", 
                    "Arboretum", 
                    "Arboretum", 
                    "A large impressionist-abstract painting in imaginative, glorious colours.", 
                    "127 x 102 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + "Ganza ganza.jpg", 
                    "Ganza ganza", 
                    "Ganza ganza", 
                    "Ganza ganza", 
                    "Abstract painting, conveying joy and celebration. Acrylic paint on canvas with a 15 mm profile.", 
                    "120 x 40 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + ".jpg", 
                    "", 
                    "", 
                    "", 
                    "Impressionist painting. Acrylic paint on canvas with a 15 mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + ".jpg", 
                    "", 
                    "", 
                    "", 
                    "Impressionist painting. Acrylic paint on canvas with a 15 mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + ".jpg", 
                    "", 
                    "", 
                    "", 
                    "Impressionist painting. Acrylic paint on canvas with a 15 mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + ".jpg", 
                    "", 
                    "", 
                    "", 
                    "Impressionist painting. Acrylic paint on canvas with a 15 mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + ".jpg", 
                    "", 
                    "", 
                    "", 
                    "Impressionist painting. Acrylic paint on canvas with a 15 mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)"
                    ),
                new ItemPosting(
                    filePath + ".jpg", 
                    "", 
                    "", 
                    "", 
                    "Impressionist painting. Acrylic paint on canvas with a 15 mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)")
            };
        }
    }
}