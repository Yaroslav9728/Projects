<?php
include_once("index.html");
$host = 'localhost';
$database = 'id1706174_bustraveldb';
$tables = 'User';
$user = 'id1706174_yaroslav9728';
$password = 'mikki2009';


function getConn() {
    return  new mysqli($host, $user, $password, $database);
}
?>