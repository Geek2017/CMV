<?php
/**
 * Template part for displaying single posts.
 *
 * @link https://codex.wordpress.org/Template_Hierarchy
 *
 * @package Monograph
 */

?>

<?php if ( has_post_thumbnail() ) : ?>
<div class="thumbnail-post-intro">
	<?php the_post_thumbnail('large'); ?>
</div><!-- .thumbnail-post-intro -->
<?php endif; ?>

<article id="post-<?php the_ID(); ?>" <?php post_class(); ?>>

	<header class="ilovewp-page-intro ilovewp-page-inner">
		<h1 class="title-page"><?php the_title(); ?></h1>
		<span class="post-meta-gravatar"><?php echo get_avatar( get_the_author_meta( 'ID' ), '60' ); ?></span>
		<p class="post-meta"><span class="posted-by"><?php _e('By','monograph'); ?> <?php echo esc_url( the_author_posts_link() ); ?></span> 
		<?php _e('in','monograph'); ?> <span class="post-meta-category"><?php the_category(esc_html_x( ', ', 'Used on archive and post pages to separate multiple categories.', 'monograph' )); ?></span>
		<span class="posted-on"><span class="genericon genericon-time"></span> <time class="entry-date published" datetime="<?php echo get_the_date('c'); ?>"><?php echo get_the_date(); ?></time></span>
		<?php if ( function_exists('the_views') ) { echo '<span class="post-views"><span class="genericon genericon-show"></span> '; the_views(); echo '</span>'; } ?>
		</p>
	</header><!-- .ilovewp-page-intro -->

	<div class="post-single clearfix">

		<?php
			the_content( /* translators: %s: Name of current post. */ sprintf(
				wp_kses( __( 'Continue reading %s <span class="meta-nav">&rarr;</span>', 'monograph' ), array( 'span' => array( 'class' => array() ) ) ),
				the_title( '<span class="screen-reader-text">"', '"</span>', false )
			) );
		?>

		<?php
			wp_link_pages( array(
				'before' => '<div class="page-links">' . esc_html__( 'Pages:', 'monograph' ),
				'after'  => '</div>',
			) );
		?>

		<?php
		$tags_list = get_the_tag_list( '', esc_html__( ', ', 'monograph' ) );
		if ( $tags_list ) {
			/* translators: %s: list of tags */
			printf( '<p class="tags-links">' . esc_html__( 'Tags: %1$s', 'monograph' ) . '</p>', $tags_list ); // WPCS: XSS OK.
		}
		?>

	</div><!-- .post-single -->

</article><!-- #post-<?php the_ID(); ?> -->