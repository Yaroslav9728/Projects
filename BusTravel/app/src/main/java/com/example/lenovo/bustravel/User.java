package com.example.lenovo.bustravel;

import java.io.Serializable;

public class User implements Serializable {
  private   String login;
   private String email;

 private static User user;
    private int id;


    public static User getInstance() {
        if(user == null) {
            user = new User();
        }

            return user;
    }
    public void setLogin(String login) {
        this.login = login;
    }
    public void setEmail(String email) {
        this.email = email;
    }
    public String getLogin() {
        return login;
    }
    public String getEmail() {
        return email;
    }
    public int getId() {
        return id;
    }
    public void setId(int id) {
        this.id = id;
    }
    public void destruct() {
        user = null;
    }
}
