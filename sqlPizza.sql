drop database if exists wpfpizzeria;
create database wpfpizzeria;

use wpfpizzeria;

create table client (
	ID int auto_increment,
	Nom varchar(500),
	Prenom varchar(500),
	Telephone int,
	DatePremiereCommande varchar(500),
    Rue varchar(500),
    Zipcode int,
    Ville varchar(500),
    primary key (ID) 
);

create table commande (
	ID int auto_increment,
    heureCmd varchar(10),
    date varchar(10),
	IDclient int,
    nomClient varchar(400),
    Total double,
	primary Key (ID)
);

create table commandeitem(
	commandeID int,
	pizza varchar(40),
	taille varchar(40),
	type varchar(40),
	boisson varchar(40),
	prix double
);

create table commis(
	ID int auto_increment,
    Nom varchar(50),
    Prenom varchar(50),
    Ville varchar(500),
    tel int,
    primary key (ID) 
);

create table livreur(
	ID int auto_increment,
    Nom varchar(50),
    Prenom varchar(50),
	Ville varchar(500),
    tel int,
    primary key (ID) 
);

create table suivicommis(
	tel int,
    idcom int
);

create table suivilivreur(
	tel int,
    idcom int
);

create table etat(
	IDcom int,
    etatcom varchar(500)
);

insert into client(Nom,Prenom,Telephone,DatePremiereCommande,Rue,Zipcode,Ville)
Values('LC','Malo',12,'12/04/2020','100 rue des lilas',95120,'Ermont');

insert into client(Nom,Prenom,Telephone,DatePremiereCommande,Rue,Zipcode,Ville)
Values('mr','henri',452577,'12/11/1999','100 rue des trains',75001,'Paris');

insert into commis(Nom, Prenom, Ville, tel) 
VALUES('Charles','Jean-luc','Sartrouville',15);

insert into livreur(Nom, Prenom, Ville, tel) 
VALUES('Carrier','Juliette','Villejuif',67);

