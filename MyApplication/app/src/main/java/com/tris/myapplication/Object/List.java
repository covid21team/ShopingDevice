package com.tris.myapplication.Object;

public class List {
    private String hoTen;
    private int id;
    private int phone;
    private String pic;

    public List(String hoTen, int id, int phone, String pic) {
        this.hoTen = hoTen;
        this.id = id;
        this.phone = phone;
        this.pic = pic;
    }

    public String getHoTen() {
        return hoTen;
    }

    public void setHoTen(String hoTen) {
        this.hoTen = hoTen;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getPhone() {
        return phone;
    }

    public void setPhone(int phone) {
        this.phone = phone;
    }

    public String getPic() {
        return pic;
    }

    public void setPic(String pic) {
        this.pic = pic;
    }
}


