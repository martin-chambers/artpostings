USE [ArtPostingsTest]
GO
DELETE FROM [dbo].[ArtPostingItems] WHERE Archive_flag = 0
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('Pasquale.jpg', 0, 'Pasquale','Pasquale','Pasquale','An abstract painting . Acrylic paint on canvas.','102 cm x 80 cm x 15 mm','£300',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('Tanyas waterlilies.jpg', 1, 'Tanya''s Waterlilies (19)','Tanya''s Waterlilies (19)','Tanya''s Waterlilies (19)','Print of an abstract acrylic painting. Can be framed.','60 x 46 x 4cm','£120',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('Blue Cow.jpg', 2, 'Blue Cow','Blue Cow','Blue Cow','Print of an abstract acrylic painting. Can be framed.','60 x 46 x 4cm','£120',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('Louisa''s Lilies.jpg', 3, 'Louisa''s Lilies','Louisa''s Lilies','Louisa''s Lilies','A large lily-flower painting in soft, restful colours.  The surface is heavily textured with flowing, effortless shapes and spirals. Acrylic paint on canvas with a 4 cm profile.','152 x 122 cm','£650',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('1sr WW lighter-001.jpg', 4, 'Over the top','Over the top','Over the top','Print of a watercolour on paper. Can be framed.','30 x 45 cm','£45',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('WW1soldiers (2nd version).jpg', 5, 'Ghosts of the Somme','Ghosts of the Somme','Ghosts of the Somme','Print of an abstract acrylic painting. Can be framed.','69 x 69 x 4 cm','£140',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('WW1Soldiers Poppies 4.jpg', 6, 'Poppies and WW1 Soldiers','Poppies and WW1 Soldiers','Poppies and WW1 Soldiers','Print of an abstract acrylic painting. Can be framed.','101 x 80 cm','£140',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('Wisteria.jpg', 7, 'Wisteria','Wisteria','Wisteria','Print of impressionist painting. Can be framed.','101 x 80 cm','£140',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('2013 Poppies1.jpg', 8, 'Poppies','Poppies','Poppies','Print of an abstract acrylic painting. Can be framed.','101 x 80 cm','£140',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('Parade.jpg', 9, 'Parade','Parade','Parade','An abstract painting, recalling all the colour and energy of this summer''s celebratory parades. Acrylic paint on deep edge canvas.','100 x 100 x 4 cm','£400',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('risdarr3.jpg', 10, 'Arrival at Risdarr','Arrival at Risdarr','Arrival at Risdarr','An abstract painting showing a peaceful, secluded place on smooth, shiny-surfaced, painted in bold, primary colours. Acrylic paint on canvas with a 4 cm profile.','122  x 91cm','£400',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('Culpepper3.jpg', 11, 'Culpepper','Culpepper','Culpepper','A large and flamboyant abstract with celebration in mind.  Difficult not to be cheerful looking at it! Acrylic paint on canvas with a 4 cm  profile.','152 x 122 cm','£650',0)
INSERT INTO [dbo].[ArtPostingItems] ([Filename],[Order],[Title],[Shortname],[Header],[Description],[Size],[Price],[Archive_flag])
     VALUES ('Jupiter3.jpg', 12, 'The Flight of Jupiter','The Flight of Jupiter','The Flight of Jupiter','An vibrant abstract painting showing energy and adventure on a textured surface, painted with bold strokes and pure colours. Acrylic paint on canvas with a 4 cm profile.','122  x 91cm','£400',0)
