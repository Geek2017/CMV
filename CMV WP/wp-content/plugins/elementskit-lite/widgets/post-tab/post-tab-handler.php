<?php
namespace ElementsKit;

class Elementskit_Widget_Post_Tab_Handler extends Core\Handler_Widget{

    static function get_name() {
        return 'elementskit-post-tab';
    }

    static function get_title() {
        return esc_html__( 'Post Tab', 'elementskit' );
    }

    static function get_icon() {
        return 'eicon-posts-grid ekit-widget-icon ';
    }

    static function get_keywords() {
        return [ 'tab', 'post tab', 'post', 'ekit', 'elementskit post tab' ];
    }

    static function get_categories() {
        return [ 'elementskit_headerfooter' ];
    }

    static function get_dir() {
        return \ElementsKit::widget_dir() . 'post-tab/';
    }

    static function get_url() {
        return \ElementsKit::widget_url() . 'post-tab/';
    }
}