# app/routes/main_routes.py (or within one of your existing route files)

from flask import Blueprint, render_template

main_bp = Blueprint('main', __name__)

@main_bp.route('/')
def index():
    return render_template('index.html')  # Render your home page template

@main_bp.route('/home')
def home():
    return render_template('home.html')  