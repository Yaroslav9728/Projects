<?php

include 'Connection.php';

$login = $_POST['login'];
 $password = $_POST['password'];
 
 $Sql_Query = "select Login from User where Login = '$login' and Password = '$password' ";
 
 $check = mysqli_fetch_array(mysqli_query($mysqli,$Sql_Query));
 
 if(isset($check)){
 
 echo "Data Matched";
 }
 else{
 echo "Invalid Username or Password Please Try Again";
 }
 
 }else{
 echo "Check Again";
 }
$mysqli = new mysqli('localhost', 'id1706174_yaroslav9728', 'mikki2009', 'id1706174_bustraveldb');
if ($mysqli->connect_errno) {
    printf("Connection is failed: %s\n", $mysqli->connect_error);
    exit();
}
$mysqli->close();

?>