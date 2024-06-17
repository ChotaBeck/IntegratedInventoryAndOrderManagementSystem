def init_routes(app):
    from .order_routes import order_bp
    from .product_routes import product_bp
    from .location_routes import location_bp
    from .user_routes import user_bp

    app.register_blueprint(order_bp)
    app.register_blueprint(product_bp)
    app.register_blueprint(location_bp)
    app.register_blueprint(user_bp)
