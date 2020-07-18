<?php

include ('Connection.php');
$name = $_POST['name'];
 $email = $_POST['email'];
 $password = $_POST['password'];
 
 $Sql_Query = "insert into User (Login,Password,Email) values ('$name','$password','$email')";
 
 if(mysqli_query($con,$Sql_Query)){
 
 echo 'Data Inserted Successfully';
 
 }
 else{
 
 echo 'Try Again';
 
 }
$mysqli = new mysqli($host, $user, $password, $database);
if ($mysqli->connect_errno) {
    printf("Connection is failed: %s\n", $mysqli->connect_error);
    exit();
}
echo json_encode($response);
$mysqli->close();
?>
