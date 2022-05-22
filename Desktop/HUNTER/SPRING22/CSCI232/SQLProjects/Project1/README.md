# CSCI232 Spring 2022 - Project 1
SQL code to create and manipulate a database based off a dataset. 
The dataset of NYC Hate Crimes was from NYC OpenData: https://data.cityofnewyork.us/Public-Safety/NYPD-Hate-Crimes/bqiq-cu78/data
Figma of the database: https://www.figma.com/file/YWUzPzbiny3tlHMx5OCk4q/CSCI232_Project-1?node-id=411%3A417

## Usage

### Sorting data
- Returning ordered report counts

Output

```
        patrolboro         | count 
---------------------------+-------
 PATROL BORO MAN SOUTH     |   295
 PATROL BORO BKLYN SOUTH   |   243
 PATROL BORO BKLYN NORTH   |   198
 PATROL BORO MAN NORTH     |   167
 PATROL BORO QUEENS NORTH  |   152
 PATROL BORO BRONX         |   110
 PATROL BORO QUEENS SOUTH  |    83
 PATROL BORO STATEN ISLAND |    48
```

### Correlations between tables
- Using inner joins to access report and arrest data

Output

```
            offense             | reports | arrests | no_arrests 
--------------------------------+---------+---------+------------
 CRIMINAL MISCHIEF & RELATED OF |     331 |      58 |        273
 MISCELLANEOUS PENAL LAW        |     327 |      55 |        272
 OFF. AGNST PUB ORD SENSBLTY &  |     164 |      43 |        121
 ASSAULT 3 & RELATED OFFENSES   |     232 |     134 |         98
 FELONY ASSAULT                 |     148 |      97 |         51
 GRAND LARCENY                  |      20 |       7 |         13
 ROBBERY                        |      38 |      26 |         12
 HARRASSMENT 2                  |      14 |       4 |         10
 PETIT LARCENY                  |       2 |       0 |          2
 BURGLARY                       |       6 |       4 |          2
 SEX CRIMES                     |       3 |       1 |          2
 FRAUDS                         |       1 |       0 |          1
 MURDER & NON-NEGL. MANSLAUGHTE |       4 |       3 |          1
 CRIMINAL TRESPASS              |       2 |       1 |          1
 INVESTIGATIONS/COMPLAINTS ONLY |       1 |       0 |          1
 RAPE                           |       1 |       1 |          0
 DANGEROUS WEAPONS              |       2 |       2 |          0
(17 rows)
```
