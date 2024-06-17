from flask import Blueprint, render_template, request, redirect, url_for, flash
from app.models import User
from app.database import db

user_bp = Blueprint('user_bp', __name__)

@user_bp.route('/users')
def users():
    users = User.query.all()
    return render_template('users.html', users=users)

@user_bp.route('/user/add', methods=['GET', 'POST'])
def add_user():
    if request.method == 'POST':
        name = request.form['name']
        role = request.form['role']
        email = request.form['email']
        new_user = User(name=name, role=role, email=email)
        db.session.add(new_user)
        db.session.commit()
        flash('User added successfully!')
        return redirect(url_for('user_bp.users'))
    return render_template('add_user.html')

@user_bp.route('/user/edit/<int:id>', methods=['GET', 'POST'])
def edit_user(id):
    user = User.query.get_or_404(id)
    if request.method == 'POST':
        user.name = request.form['name']
        user.role = request.form['role']
        user.email = request.form['email']
        db.session.commit()
        flash('User updated successfully!')
        return redirect(url_for('user_bp.users'))
    return render_template('edit_user.html', user=user)

@user_bp.route('/user/delete/<int:id>')
def delete_user(id):
    user = User.query.get_or_404(id)
    db.session.delete(user)
    db.session.commit()
    flash('User deleted successfully!')
    return redirect(url_for('user_bp.users'))
