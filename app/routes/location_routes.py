from flask import Blueprint, render_template, request, redirect, url_for, flash, jsonify
from app.models import Location
from app.database import db

location_bp = Blueprint('location', __name__)

@location_bp.route('/locations')
def locations():
    locations = Location.query.all()
    return render_template('locations.html', locations=locations)

@location_bp.route('/location/add', methods=['GET', 'POST'])
def add_location():
    if request.method == 'POST':
        name = request.form['name']
        capacity = request.form['capacity']
        new_location = Location(name=name, capacity=capacity)
        db.session.add(new_location)
        db.session.commit()
        flash('Location added successfully!')
        return redirect(url_for('location.locations'))
    return render_template('add_location.html')

@location_bp.route('/location/edit/<int:id>', methods=['GET', 'POST'])
def edit_location(id):
    location = Location.query.get_or_404(id)
    if request.method == 'POST':
        location.name = request.form['name']
        location.capacity = request.form['capacity']
        db.session.commit()
        flash('Location updated successfully!')
        return redirect(url_for('location_.locations'))
    return render_template('edit_location.html', location=location)

@location_bp.route('/location/delete/<int:id>')
def delete_location(id):
    location = Location.query.get_or_404(id)
    db.session.delete(location)
    db.session.commit()
    flash('Location deleted successfully!')
    return redirect(url_for('location.locations'))
