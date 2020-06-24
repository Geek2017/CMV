<?php

/*
Plugin Name: Ultimate Custom ScrollBar
Plugin URI: http://themespick.com/plugins/ultimate-custom-scrollbar
Description: Using Ultimate Custom ScrollBar for customize scrollbars, you get a very elegant and unique site. Consistent scroll behavior for every device. You can change scrollbar size, colors, animation, position from this settings page.
Author: ThemesPick
Author URI: http://themespick.com
Version: 1.2
textdomain: ultimate-custom-scrollbar
*/

if ( !defined( 'ABSPATH' ) ) {
    exit;
    // Exit if accessed directly
}

// Translate direction
load_plugin_textdomain( 'ultimate-custom-scrollbar', false, dirname( plugin_basename( __FILE__ ) ) . '/languages/' );
// Defines
define( 'UCS_VERSION', '1.2' );
define( 'UCS_ACC_URL', WP_PLUGIN_URL . '/' . plugin_basename( dirname( __FILE__ ) ) . '/' );
define( 'UCS_ACC_PATH', plugin_dir_path( __FILE__ ) );
// Files
function uts_assets_files()
{
    $options = get_option( 'ucs_options_tab' );
    
    if ( !empty($options) ) {
        
        if ( 'enable' === $options['ucs_control'] ) {
            wp_enqueue_style(
                'uts-stylesheet',
                UCS_ACC_URL . 'assets/css/main.css',
                null,
                time(),
                'all'
            );
            wp_enqueue_script(
                'uts-nicescroll',
                UCS_ACC_URL . 'assets/js/jquery.nicescroll.min.js',
                array(),
                '3.7.6',
                true
            );
            
			$uts_active = '
				(function($){ "use strict";
					$("body").niceScroll({
						background: "' . (( !empty($options['ucs_bgcolor']) ? esc_html( $options['ucs_bgcolor'] ) : '#f1f1f1' )) . '",
						cursorcolor:"' . (( !empty($options['ucs_cursorcolor']) ? esc_html( $options['ucs_cursorcolor'] ) : '#1FC8DF' )) . '",
						cursorwidth:"' . (( !empty($options['ucs_cursorwidth']) ? esc_html( $options['ucs_cursorwidth'] ) : '10px' )) . '",
						autohidemode: "' . (( !empty($options['ucs_autohide']) ? esc_html( $options['ucs_autohide'] ) : 'false' )) . '",
						scrollspeed: ' . (( !empty($options['ucs_scrollspeed']) ? esc_html( $options['ucs_scrollspeed'] ) : 60 )) . ',
						mousescrollstep: ' . (( !empty($options['ucs_mousescrollstep']) ? esc_html( $options['ucs_mousescrollstep'] ) : 40 )) . ',
						cursorborder:"' . (( !empty($options['ucs_cursorborder']) ? esc_html( $options['ucs_cursorborder'] ) : '0px solid #ddd' )) . '",
						cursorborderradius: "' . (( !empty($options['ucs_cursorborderradius']) ? esc_html( $options['ucs_cursorborderradius'] ) : '5px' )) . '",
						emulatetouch: ' . (( !empty($options['ucs_emulatetouch']) ? esc_html( $options['ucs_emulatetouch'] ) : 'false' )) . ',
						cursorminheight: ' . (( !empty($options['ucs_cursorminheight']) ? esc_html( $options['ucs_cursorminheight'] ) : 32 )) . ',
						bouncescroll: ' . (( !empty($options['ucs_bouncescroll']) ? esc_html( $options['ucs_bouncescroll'] ) : 'false' )) . ',
						horizrailenabled: ' . (( !empty($options['ucs_horizrailenabled']) ? esc_html( $options['ucs_horizrailenabled'] ) : 'false' )) . ',
						railalign: "' . (( !empty($options['ucs_railalign']) ? esc_html( $options['ucs_railalign'] ) : 'right' )) . '",
						railvalign: "' . (( !empty($options['ucs_railvalign']) ? esc_html( $options['ucs_railvalign'] ) : 'bottom' )) . '",
						enablemousewheel: ' . (( !empty($options['ucs_enablemousewheel']) ? esc_html( $options['ucs_enablemousewheel'] ) : 'true' )) . ',
						enablekeyboard: ' . (( !empty($options['ucs_enablekeyboard']) ? esc_html( $options['ucs_enablekeyboard'] ) : 'true' )) . ',
						hidecursordelay: ' . (( !empty($options['ucs_hidecursordelay']) ? esc_html( $options['ucs_hidecursordelay'] ) : 400 )) . ',
						enablescrollonselection: ' . (( !empty($options['ucs_enablescrollonselection']) ? esc_html( $options['ucs_enablescrollonselection'] ) : 400 )) . ',
						cursordragspeed: ' . (( !empty($options['ucs_cursordragspeed']) ? esc_html( $options['ucs_cursordragspeed'] ) : 0.3 )) . ',
						zindex: "auto"
					});
				})(jQuery);
			';
            
            wp_add_inline_script( 'uts-nicescroll', $uts_active );
        }
    
    } else {
        wp_enqueue_style(
            'uts-stylesheet',
            UCS_ACC_URL . 'assets/css/main.css',
            null,
            time(),
            'all'
        );
        wp_enqueue_script(
            'uts-nicescroll',
            UCS_ACC_URL . 'assets/js/jquery.nicescroll.min.js',
            array(),
            '3.7.6',
            true
        );
        $uts_active = '
			(function($){ "use strict";
				$("body").niceScroll({
					background: "#f1f1f1",
					cursorcolor:"#1FC8DF",
					cursorwidth:"10px",
			        scrollspeed: 60,
			        mousescrollstep: 40,
					cursorborder:"0px solid #ddd",
					cursorborderradius: "5px",
					autohidemode: false,
					zindex: "auto"
				});
			})(jQuery);
		';
        wp_add_inline_script( 'uts-nicescroll', $uts_active );
    }

}

add_action( 'wp_enqueue_scripts', 'uts_assets_files' );

// Include Options
require_once UCS_ACC_PATH . '/inc/class.settings-api.php';
require_once UCS_ACC_PATH . '/inc/settings-page.php';