<?php
/**
* WordPress settings API
*/
if ( ! defined( 'ABSPATH' ) ) {
    exit; // Exit if accessed directly
}
if ( !class_exists('UCS_Settings_API' ) ):
class UCS_Settings_API {

    private $settings_api;

    function __construct() {
        $this->settings_api = new WeDevs_Settings_API;

        add_action( 'admin_init', array($this, 'admin_init') );
        add_action( 'admin_menu', array($this, 'admin_menu') );
    }

    function admin_init() {

        //set the settings
        $this->settings_api->set_sections( $this->get_settings_sections() );
        $this->settings_api->set_fields( $this->get_settings_fields() );

        //initialize settings
        $this->settings_api->admin_init();
    }

    function admin_menu() {
        add_options_page( 'Ultimate Custom ScrollBar', 'Ultimate Custom ScrollBar', 'delete_posts', 'ultimate_scrollbar_settings', array($this, 'plugin_page') );
    }

    function get_settings_sections() {
        $sections = array(
            array(
                'id'    => 'ucs_options_tab',
                'title' => __( 'ScrollBar Options', 'ultimate-custom-scrollbar' )
            )
        );
        return $sections;
    }

    /**
     * Returns all the settings fields
     *
     * @return array settings fields
     */
    function get_settings_fields() {
        $settings_fields = array(
            'ucs_options_tab' => array(
                array(
                    'name'    => 'ucs_control',
                    'label'   => __( 'Enable/Disable ScrollBar', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can enable or disable scrollbar using this option.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'radio',
                    'default' => 'enable',
                    'options' => array(
                        'enable' => 'Enable',
                        'disable'  => 'Disable'
                    )
                ),
                array(
                    'name'              => 'ucs_cursorwidth',
                    'label'             => __( 'Cursor Width', 'ultimate-custom-scrollbar' ),
                    'desc'              => __( 'You can increase or decrease cursor width using this option.', 'ultimate-custom-scrollbar' ),
                    'type'              => 'text',
                    'default'           => '16px'
                ),
                array(
                    'name'    => 'ucs_bgcolor',
                    'label'   => __( 'Rail Background Color', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can change rail background color from this color picker.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'color',
                    'default' => '#f1f1f1'
                ),
                array(
                    'name'    => 'ucs_cursorcolor',
                    'label'   => __( 'Cursor Color', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can change cursor color from this color picker.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'color',
                    'default' => '#1FC8DF'
                ),
                array(
                    'name'    => 'ucs_autohide',
                    'label'   => __( 'Auto Hide Mode', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can select scrollbar auto hide mode from this selectbox.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'select',
                    'default' => 'true',
                    'options' => array(
                        'true'  => __( 'Hide when no scrolling', 'ultimate-custom-scrollbar' ),
                        'cursor'    => __( 'Only cursor hidden', 'ultimate-custom-scrollbar' ),
                        'false' => __( 'Do not hide', 'ultimate-custom-scrollbar' ),
                        'leave' => __( 'Hide only if pointer leaves content', 'ultimate-custom-scrollbar' ),
                        'hidden'    => __( 'Hide always', 'ultimate-custom-scrollbar' ),
                        'scroll'    => __( 'Show only on scroll', 'ultimate-custom-scrollbar' )
                    )
                ),
				array(
                    'name'              => 'ucs_cursorborder',
                    'label'             => __( 'Cursor Border', 'ultimate-custom-scrollbar' ),
                    'desc'              => __( 'You can control cursor border using this option. Default is 0px solid #ddd.', 'ultimate-custom-scrollbar' ),
                    'type'              => 'text',
                    'default'           => '0px solid #ddd'
                ),
                array(
                    'name'              => 'ucs_cursorborderradius',
                    'label'             => __( 'Cursor Border Radius', 'ultimate-custom-scrollbar' ),
                    'desc'              => __( 'You can control cursor border radius using this option ( Pro only ).', 'ultimate-custom-scrollbar' ),
                    'type'              => 'text',
                    'default'           => '5px'
                ),
                array(
                    'name'              => 'ucs_scrollspeed',
                    'label'             => __( 'Scroll Speed', 'ultimate-custom-scrollbar' ),
                    'desc'              => __( 'You can control scroll speed using this option ( Pro only ).', 'ultimate-custom-scrollbar' ),
                    'type'              => 'number',
                    'default'           => 60
                ),
                array(
                    'name'              => 'ucs_mousescrollstep',
                    'label'             => __( 'Mouse Scroll Step', 'ultimate-custom-scrollbar' ),
                    'desc'              => __( 'You can control mouse scroll step using this option ( Pro only ).', 'ultimate-custom-scrollbar' ),
                    'type'              => 'number',
                    'default'           => 40
                ),
                array(
                    'name'    => 'ucs_emulatetouch',
                    'label'   => __( 'Emulate Touch', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'Enable cursor-drag scrolling like touch devices in desktop computer.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'radio',
                    'default' => 'false',
                    'options' => array(
                        'true' => 'Enable',
                        'false'  => 'Disable'
                    )
                ),
                array(
                    'name'              => 'ucs_cursorminheight',
                    'label'             => __( 'Minimum Cursor Height', 'ultimate-custom-scrollbar' ),
                    'desc'              => __( 'You can set the minimum cursor height.', 'ultimate-custom-scrollbar' ),
                    'type'              => 'number',
                    'default'           => 32
                ),
                array(
                    'name'    => 'ucs_bouncescroll',
                    'label'   => __( 'Bounce Scroll', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'Enable scroll bouncing at the end of content as mobile-like.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'radio',
                    'default' => 'false',
                    'options' => array(
                        'true' => 'Enable',
                        'false'  => 'Disable'
                    )
                ),
                array(
                    'name'    => 'ucs_horizrailenabled',
                    'label'   => __( 'Horizrail Enabled', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can control horizontal scroll.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'radio',
                    'default' => 'true',
                    'options' => array(
                        'true' => 'Enable',
                        'false'  => 'Disable'
                    )
                ),
                array(
                    'name'              => 'ucs_railalign',
                    'label'             => __( 'Rail Align', 'ultimate-custom-scrollbar' ),
                    'desc'              => __( 'You can control alignment of vertical rail ( Pro only ).', 'ultimate-custom-scrollbar' ),
                    'type'              => 'text',
                    'default'           => 'Pro',
                    'pro' => true
                ),
                array(
                    'name'    => 'ucs_railalign',
                    'label'   => __( 'Rail Align', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can control alignment of vertical rail.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'radio',
                    'default' => 'right',
                    'options' => array(
                        'right' => 'Right',
                        'left'  => 'Left'
                    )
                ),
                array(
                    'name'    => 'ucs_railvalign',
                    'label'   => __( 'Rail Valign', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can control alignment of horizontal rail.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'radio',
                    'default' => 'bottom',
                    'options' => array(
                        'top' => 'Top',
                        'bottom'  => 'Bottom'
                    )
                ),
                array(
                    'name'    => 'ucs_enablemousewheel',
                    'label'   => __( 'Enable Mousewheel', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can control Mousewheel using this option.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'radio',
                    'default' => 'true',
                    'options' => array(
                        'true' => 'Enable',
                        'false'  => 'Disable'
                    )
                ),
                array(
                    'name'    => 'ucs_enablekeyboard',
                    'label'   => __( 'Enable Keyboard', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can enable or disable keyboard events using this option.', 'ultimate-custom-scrollbar' ),
                    'type'    => 'radio',
                    'default' => 'true',
                    'options' => array(
                        'true' => 'Enable',
                        'false'  => 'Disable'
                    )
                ),
                array(
                    'name'              => 'ucs_hidecursordelay',
                    'label'             => __( 'Hide Cursor Delay', 'ultimate-custom-scrollbar' ),
                    'desc'              => __( 'You can set the delay in microseconds to fading out scrollbar.', 'ultimate-custom-scrollbar' ),
                    'type'              => 'number',
                    'default'           => 400
                ),
                array(
                    'name'    => 'ucs_enablescrollonselection',
                    'label'   => __( 'Enable Scroll On Selection', 'ultimate-custom-scrollbar' ),
                    'desc'    => __( 'You can enable auto-scrolling of content when selection text .', 'ultimate-custom-scrollbar' ),
                    'type'    => 'radio',
                    'default' => 'true',
                    'options' => array(
                        'true' => 'Enable',
                        'false'  => 'Disable'
                    )
                ),
                array(
                    'name'              => 'ucs_cursordragspeed',
                    'label'             => __( 'Cursor Drag Speed', 'ultimate-custom-scrollbar' ),
                    'desc'              => __( 'You can set speed of selection when dragged with cursor ( Pro only ).', 'ultimate-custom-scrollbar' ),
                    'type'              => 'text',
                    'default'           => 0.3
                )
            )
        );

        return $settings_fields;
    }

    function plugin_page() {
        ?>
        <div class="tp-options-header">
            <h2><?php echo 'Ultimate Custom ScrollBar'; ?></h2>
            <div class="tp-author"><?php _e('By ', 'ultimate-custom-scrollbar'); ?> <a href="<?php echo esc_url('http://www.themespick.com'); ?>" target="_blank"><?php echo 'ThemesPick'; ?></a></div>
            <div class="ucs-version"><span><?php _e('Version: ', 'ultimate-custom-scrollbar'); ?></span><?php echo UCS_VERSION; ?></div>
        </div>
        <?php
        echo '<div class="wrap">';
        $this->settings_api->show_navigation();
        $this->settings_api->show_forms();

        echo '</div>';
    }

    /**
     * Get all the pages
     *
     * @return array page names with key value pairs
     */
    function get_pages() {
        $pages = get_pages();
        $pages_options = array();
        if ( $pages ) {
            foreach ($pages as $page) {
                $pages_options[$page->ID] = $page->post_title;
            }
        }

        return $pages_options;
    }

}
endif;
new UCS_Settings_API();