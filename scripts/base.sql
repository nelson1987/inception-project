CREATE DATABASE basegeografica;

\c basegeografica;

CREATE TABLE "Customers" (
    "Id" INT GENERATED ALWAYS AS IDENTITY NOT NULL,
    "FirstName" VARCHAR(255) NOT NULL,
    "LastName" VARCHAR(255) NOT NULL,
    "Email" VARCHAR(255) NOT NULL,
    "Address" VARCHAR(255) NOT NULL,
    "City" VARCHAR(255) NOT NULL,
    CONSTRAINT "PK_Customer" PRIMARY KEY ("Id")
);
