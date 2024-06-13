from flask import Blueprint, render_template

auth = Blueprint('auth', __name__)

#Login route
@auth.route('/Login')
def Login():
    return render_template('Login.html')

#Logout route
@auth.route('/Logout')
def Logout():
    return "<p>Logout</p>"