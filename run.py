from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from flask_migrate import Migrate
from app.database import db 
import app.routes.user_routes as user_routes
import app.routes.product_routes as product_routes
import app.routes.location_routes as location_routes
import app.routes.order_routes as order_routes
import app.routes.main_routes as main_routes
from flask import redirect, url_for


migrate = Migrate()

app = Flask(__name__,template_folder='app/templates', static_folder='app/static')
app.config.from_object('config.Config')

db.init_app(app)
migrate.init_app(app, db)



# Register blueprints (importing directly from each module)
app.register_blueprint(user_routes.user_bp)  
app.register_blueprint(product_routes.product_bp)
app.register_blueprint(location_routes.location_bp)
app.register_blueprint(order_routes.order_bp)
app.register_blueprint(main_routes.main_bp)


@app.route('/')  # Root URL
def index():
    return redirect(url_for('main.index'))  # Redirect to the 'home' function in your main blueprint

if __name__ == "__main__":
    app.run(debug=True)
