
-- Create timesheet table
Use MyTimeSheet
create table timesheet (id int identity(1,1) primary key not null, 
week int not null,
name varchar(50) not null,
pic varchar(MAX),
ps varchar(MAX),
time varchar(50))

-- Create Detail table
Use MyTimeSheet
create table Detail (week int not null,
pid int not null,
text varchar(MAX),
hours float,
name varchar(50),
id int identity(1,1) primary key not null)

-- Insert Data to timesheet
Use MyTimeSheet
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(14,'Teddy Long','','this is PS at week 14','2011 01 01')

INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(15,'Teddy Long','','this is PS at week 15','2011 02 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(15,'Eric Dai','','this is PS at week 15','2011 02 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(15,'Spark Yao','','this is PS at week 15','2011 02 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(15,'Weisong Wang','','this is PS at week 15','2011 03 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(15,'Lyn Zhou','','this is PS at week 15','2011 03 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(15,'Cindy Liu','','this is PS at week 15','2011 03 01')

INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(16,'Teddy Long','','this is PS at week 15','2011 02 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(16,'Eric Dai','','this is PS at week 15','2011 02 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(16,'Spark Yao','','this is PS at week 15','2011 02 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(16,'Weisong Wang','','this is PS at week 15','2011 03 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(16,'Lyn Zhou','','this is PS at week 15','2011 03 01')
INSERT INTO [MyTimeSheet].[dbo].[timesheet]
([week],[name],[pic],[ps],[time])VALUES(16,'Cindy Liu','','this is PS at week 15','2011 03 01')

-- Insert Data to Detail
Use MyTimeSheet
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(14,1,'Task one',8,'Teddy Long')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(14,2,'Task two',8,'Teddy Long')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,1,'Task one',8,'Teddy Long')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,2,'Task two',8,'Teddy Long')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,3,'Task three',8,'Teddy Long')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,1,'Task one',8,'Eric Dai')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,2,'Task two',8,'Eric Dai')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,3,'Task one',8,'Eric Dai')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,1,'Task one',8,'Spark Yao')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,2,'Task two',8,'Spark Yao')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,3,'Task three',8,'Spark Yao')

INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,1,'Task one',8,'Weisong Wang')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,2,'Task two',8,'Weisong Wang')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(15,3,'Task three',8,'Weisong Wang')
           
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,1,'Task one',8,'Teddy Long')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,2,'Task two',8,'Teddy Long')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,3,'Task three',8,'Teddy Long')
           
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,1,'Site Map Testing',16,'Teddy Long')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,2,'SkyPad VM setup',16,'Teddy Long')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,3,'Other Testing',8,'Teddy Long')
           
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,1,'Project One kick off',16,'Eric Dai')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,2,'Project Two kick off',16,'Eric Dai')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,3,'Project Three kick off',8,'Eric Dai')
           
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,1,'Project One Testing',16,'Spark Yao')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,2,'Project Two Testing',16,'Spark Yao')
           
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,1,'Diet',40,'Weisong Wang')      
           
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,1,'Project One Testing',24,'Lyn Zhou')
INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,2,'Project Two Testing',16,'Lyn Zhou')    

INSERT INTO [MyTimeSheet].[dbo].[Detail]
           ([week],[pid],[text],[hours],[name])VALUES(16,1,'Go Home',40,'Cindy Liu')
                      