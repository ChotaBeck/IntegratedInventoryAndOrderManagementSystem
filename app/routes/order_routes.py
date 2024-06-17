from flask import Blueprint, render_template, request, redirect, url_for, flash
from app.models import Order
from app import db

order_bp = Blueprint('order_bp', __name__)

@order_bp.route('/orders')
def orders():
    orders = Order.query.all()
    return render_template('orders.html', orders=orders)

@order_bp.route('/order/add', methods=['GET', 'POST'])
def add_order():
    if request.method == 'POST':
        customer_id = request.form['customer_id']
        order_date = request.form['order_date']
        status = request.form['status']
        shipping_address = request.form['shipping_address']
        new_order = Order(customer_id=customer_id, order_date=order_date, status=status, shipping_address=shipping_address)
        db.session.add(new_order)
        db.session.commit()
        flash('Order added successfully!')
        return redirect(url_for('order_bp.orders'))
    return render_template('add_order.html')

@order_bp.route('/order/edit/<int:id>', methods=['GET', 'POST'])
def edit_order(id):
    order = Order.query.get_or_404(id)
    if request.method == 'POST':
        order.customer_id = request.form['customer_id']
        order.order_date = request.form['order_date']
        order.status = request.form['status']
        order.shipping_address = request.form['shipping_address']
        db.session.commit()
        flash('Order updated successfully!')
        return redirect(url_for('order_bp.orders'))
    return render_template('edit_order.html', order=order)

@order_bp.route('/order/delete/<int:id>')
def delete_order(id):
    order = Order.query.get_or_404(id)
    db.session.delete(order)
    db.session.commit()
    flash('Order deleted successfully!')
    return redirect(url_for('order_bp.orders'))
