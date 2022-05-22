-- Georgina Woo
-- georginawooxy@gmail.com
-- May 21 2022
-- Project 2: Youtube database

-- creating database
create database youtube with owner = '';

--creating tables
-- Figma: https://www.figma.com/file/YWUzPzbiny3tlHMx5OCk4q/CSCI232_Project-1?node-id=455%3A663

create table comments
(
	commentid int primary key,
	vid int,
	comment varcharr,
	userid int,
	commentdate date
);

create table channels
(
	channelid int primary key,
	userid int,
	subscribers int,
	channelname varchar,
	numPlaylist int,
	channel_url varchar,
	creation_date date
);

create table users
(
	username varchar,
	userid int primary key,
	user-bio varchar,
	creation_date date,
	phonenumber varchar,
	email varchar
);

create table video_details
(
    vid int primary key,
    title varchar,
    video_url varchar,
    description varchar,
    views int,
    likes int,
    dislikes int,
    date_posted date,
    numcomments int,
    channeled int,
    genreid int
);

create table genre
(
    genreid int primary key,
    genrename varchar
);

--Uploading data: \copy [tablename] from '[filename]' DELIMITER ',' CSV HEADER

--1. Top 3 channels that have most subscribers?
select channelname, subscribers from channels order by subscribers desc limit 3;

--2. Genre(s) with least views?
with table1 as 
(
	select views 
	from video_details 
	order by views asc limit 1
)
select genreid, count(genreid) 
from video_details 
where video_details.views = table1.veiws 
group by genreid;

--3. What video has the most dislikes
Select vid, description from video_details order by dislikes desc limit=1;

--4. Which are the most popular users in each genre?
with table1 as (select genreid, channelid, subscribers from video_details inner join channels on video_details.channelid = channels.channelid)
select table1.genreid, table1.channelid, max(table1.subscribers) from table1
group by table1.genreid;


--5. What genres have the most popular videos?
select genrename, views
from video_details v inner join genre g on v.genreid = g.genreid
group by genrename
order by views;

--6. What’s the most popular genre in each country?
select country, genrename, views 
from users u inner join channels c on u.userid = c.userid
inner join video_details v on c.channelid = v.channelid
inner join genre g on c.genreid = g.genreid
groupby country, genrename
order by views;






