<?php
/**
 * The base configuration for WordPress
 *
 * The wp-config.php creation script uses this file during the
 * installation. You don't have to use the web site, you can
 * copy this file to "wp-config.php" and fill in the values.
 *
 * This file contains the following configurations:
 *
 * * MySQL settings
 * * Secret keys
 * * Database table prefix
 * * ABSPATH
 *
 * @link https://codex.wordpress.org/Editing_wp-config.php
 *
 * @package WordPress
 */

// ** MySQL settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define( 'DB_NAME', 'CMV' );

/** MySQL database username */
define( 'DB_USER', 'root' );

/** MySQL database password */
define( 'DB_PASSWORD', 'r00t' );

/** MySQL hostname */
define( 'DB_HOST', 'localhost' );

/** Database Charset to use in creating database tables. */
define( 'DB_CHARSET', 'utf8mb4' );

/** The Database Collate type. Don't change this if in doubt. */
define( 'DB_COLLATE', '' );

/**#@+
 * Authentication Unique Keys and Salts.
 *
 * Change these to different unique phrases!
 * You can generate these using the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}
 * You can change these at any point in time to invalidate all existing cookies. This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define( 'AUTH_KEY',         '< ?i?UQ(%zJW%5RAi$|?ju{^xk%8(7w{2VDq]Y)wA`R$iGT]eTa~0}N7Vzlp/U#/' );
define( 'SECURE_AUTH_KEY',  'f#FTx5RSv0D[j*~d0aJtTJgnvIQ}PvsAnnN(m2D.LN3h{,8KrY?sfqu{/pd=dr~y' );
define( 'LOGGED_IN_KEY',    '_FRC8(EF*<fg4Y!Z/}Aa.s U4_3j`7M{j: s]{Zd48IB$i7/![VGa$Pxt+[.^%LD' );
define( 'NONCE_KEY',        '!/x|+a(MW{lCmz`;JPW,3 SwRCBJqaR?x3#i#ze*I|^-F)PVkqqaE3[=7Nh]~fpQ' );
define( 'AUTH_SALT',        'ZBIExLrwRv0l9veNv!O3ud$va7W>Js+@rO>`X[_@5-}6CujAt]#kcKfjF^bT8Sex' );
define( 'SECURE_AUTH_SALT', 'CDMjpy{mFtGP*>2/Tx45yHCQKlqqj_pzY}D}A`*r6VN9z7c4ZZAx:W<8{YA=T=w>' );
define( 'LOGGED_IN_SALT',   'd0j})-=oS 5CZb931Goe0T*4Ct%M9!%S$~pfxR3D=g5TC;CX.!JNOx5e;SQ>CG0C' );
define( 'NONCE_SALT',       '?BY|oeMaLd,mJb}^($Fc@=#h1Nf~(TW*>$=urMPT>VC$z)zr&c(8P=a >jxI>~i_' );

/**#@-*/

/**
 * WordPress Database Table prefix.
 *
 * You can have multiple installations in one database if you give each
 * a unique prefix. Only numbers, letters, and underscores please!
 */
$table_prefix = 'wp_';

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 *
 * For information on other constants that can be used for debugging,
 * visit the Codex.
 *
 * @link https://codex.wordpress.org/Debugging_in_WordPress
 */
define( 'WP_DEBUG', false );

/* That's all, stop editing! Happy publishing. */

/** Absolute path to the WordPress directory. */
if ( ! defined( 'ABSPATH' ) ) {
	define( 'ABSPATH', dirname( __FILE__ ) . '/' );
}

/** Sets up WordPress vars and included files. */
require_once( ABSPATH . 'wp-settings.php' );
