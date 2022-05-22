-- Georgina Woo
-- georginawooxy@gmail.com
-- Feb 12 2022
-- Project 1: Hate Crime stats in NYC.

-- creating database
create database crimestats with owner = '';

--creating tables
-- Figma: https://www.figma.com/file/YWUzPzbiny3tlHMx5OCk4q/CSCI232_Project-1?node-id=411%3A417

create table crimetime
(
	cid int primary key,
	year int,
	month int,
	crime_date varchar
);

create table crimestats_overview
(
	cid int primary key,
	crime_date varchar,
	patrolboro varchar
);

create table crime_location
(
	cid int primary key,
	precintcode int,
	patrolboro varchar,
	county varchar
);

create table offensedetails
(
	cid int primary key,
	category varchar,
	offense_desc varchar,
	pd_desc varchar,
	bias_motive varchar,
	bias_category varchar
);

create table arrest_details
(
	cid int primary key,
	a_date varchar,
	aid varchar
);

--Uploading data: \copy [tablename] from '[filename]' DELIMITER ',' CSV HEADER

--Which type of complaints led to the most arrests?

select offense_desc as offense, 
count(offense_desc) as reports, 
count(aid) as arrests
from offensedetails o
inner join arrest_details a on o.cid=a.cid
group by offense_desc
order by arrests desc
;


--Which type of complaints led to the least arrests?
select offense_desc as offense, 
count(offense_desc) as reports, 
count(aid) as arrests
from offensedetails o
inner join arrest_details a on o.cid=a.cid
group by offense_desc
order by arrests asc
;

--Most complaints without arrests
select offense_desc as offense, 
count(offense_desc) as reports, 
count(aid) as arrests, 
count(offense_desc)-count(aid) as no_arrests
from offensedetails o inner join arrest_details a on o.cid=a.cid 
group by offense_desc 
order by no_arrests desc;


--Which area has the most crime?
select county, patrolboro, count(cid) 
from crime_location
group by patrolboro, county
order by count desc
;

--which month had the most crime?
select month, count(offense_desc) as offences
from crimetime c inner join offensedetails o on c.cid=o.cid 
group by month
order by offences desc;
	

-- Which area had the most crimes?
select patrolboro, count(cid) from crimestats_overview group by patrolboro order by count desc;


