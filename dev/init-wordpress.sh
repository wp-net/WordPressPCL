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

rm -f "$WP_READY_FILE"

echo "Waiting for WordPress installation to complete..."
until wp core is-installed --path="$WP_PATH" >/dev/null 2>&1; do
    echo "Installing WordPress..."
    if wp core install \
        --path="$WP_PATH" \
        --url="$WP_URL" \
        --title="$WP_TITLE" \
        --admin_user="$WP_ADMIN_USER" \
        --admin_password="$WP_ADMIN_PASSWORD" \
        --admin_email="$WP_ADMIN_EMAIL"; then
        break
    fi

    sleep 2
done

echo "Configuring WordPress for tests..."
wp option update permalink_structure "/%postname%" --path="$WP_PATH"
wp plugin install application-passwords-enable --activate --force --path="$WP_PATH"
wp plugin install contact-form-7 --activate --force --path="$WP_PATH"
wp plugin install jwt-auth --activate --force --path="$WP_PATH"

echo "Registering test meta fields..."
mkdir -p "$WP_PATH/wp-content/mu-plugins"
cat > "$WP_PATH/wp-content/mu-plugins/wordpresspcl-test-meta.php" << 'EOF'
<?php
add_action('rest_api_init', function () {
    register_post_meta('post', 'wordpresspcl_test_meta', [
        'type'         => 'string',
        'single'       => true,
        'show_in_rest' => true,
        'default'      => '',
    ]);
});
EOF

touch "$WP_READY_FILE"
echo "WordPress test environment is ready."
