from flask import Blueprint, render_template, request, redirect, url_for, flash
from app.models import Product, Location
from app import db

product_bp = Blueprint('product_bp', __name__)

@product_bp.route('/products')
def products():
    products = Product.query.all()
    return render_template('products.html', products=products)

@product_bp.route('/product/add', methods=['GET', 'POST'])
def add_product():
    if request.method == 'POST':
        name = request.form['name']
        quantity = request.form['quantity']
        location_id = request.form['location_id']
        status = request.form['status']
        new_product = Product(name=name, quantity=quantity, location_id=location_id, status=status)
        db.session.add(new_product)
        db.session.commit()
        flash('Product added successfully!')
        return redirect(url_for('product_bp.products'))
    locations = Location.query.all()  # Fetch locations to display in the form
    return render_template('add_product.html', locations=locations)

@product_bp.route('/product/edit/<int:id>', methods=['GET', 'POST'])
def edit_product(id):
    product = Product.query.get_or_404(id)
    if request.method == 'POST':
        product.name = request.form['name']
        product.quantity = request.form['quantity']
        product.location_id = request.form['location_id']
        product.status = request.form['status']
        db.session.commit()
        flash('Product updated successfully!')
        return redirect(url_for('product_bp.products'))
    locations = Location.query.all()  # Fetch locations to display in the form
    return render_template('edit_product.html', product=product, locations=locations)

@product_bp.route('/product/delete/<int:id>')
def delete_product(id):
    product = Product.query.get_or_404(id)
    db.session.delete(product)
    db.session.commit()
    flash('Product deleted successfully!')
    return redirect(url_for('product_bp.products'))
