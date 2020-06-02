drop database if exists DSusers;

create database DSusers;

use DSusers; 



-- auto-generated definition
create table users
(
    USER     text not null,
    PASSWORD text not null,
    SALT     text null
)
    collate = utf8mb4_unicode_ci;
