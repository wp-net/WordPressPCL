#!/bin/sh

set -eu

WP_PATH="${WORDPRESS_SETUP_PATH:-/var/www/html}"
WP_URL="${WORDPRESS_SETUP_URL:-http://localhost:8080}"
WP_TITLE="${WORDPRESS_SETUP_TITLE:-Test Instance}"
WP_ADMIN_USER="${WORDPRESS_SETUP_ADMIN_USER:-wordpress}"
WP_ADMIN_PASSWORD="${WORDPRESS_SETUP_ADMIN_PASSWORD:-wordpress}"
WP_ADMIN_EMAIL="${WORDPRESS_SETUP_ADMIN_EMAIL:-test@example.com}"
WP_READY_FILE="${WORDPRESS_READY_FILE:-$WP_PATH/.wp-tests-ready}"

echo "Waiting for WordPress files to be provisioned..."
until [ -f "$WP_PATH/wp-config.php" ]; do
    sleep 2
done

echo "Waiting for database connectivity..."
until wp db check --path="$WP_PATH" >/dev/null 2>&1; do
    sleep 2
done

if ! wp core is-installed --path="$WP_PATH" >/dev/null 2>&1; then
    echo "Installing WordPress..."
    wp core install \
        --path="$WP_PATH" \
        --url="$WP_URL" \
        --title="$WP_TITLE" \
        --admin_user="$WP_ADMIN_USER" \
        --admin_password="$WP_ADMIN_PASSWORD" \
        --admin_email="$WP_ADMIN_EMAIL"
fi

echo "Configuring WordPress for tests..."
wp option update permalink_structure "/%postname%" --path="$WP_PATH"
wp plugin install application-passwords-enable --activate --force --path="$WP_PATH"
wp plugin install contact-form-7 --activate --force --path="$WP_PATH"
wp plugin install jwt-auth --activate --force --path="$WP_PATH"

touch "$WP_READY_FILE"
echo "WordPress test environment is ready."
