<?php
/**
 * The template used for displaying featured posts on the Front Page.
 *
 * @package Monograph
 */
?>

<?php
	
	$featured_tag = get_theme_mod( 'monograph_exclusive_term_1', 'exclusive' );
	
	$custom_loop = new WP_Query( array(
		'post_type'      => 'post',
		'posts_per_page' => 1,
		'order'          => 'DESC',
		'orderby'        => 'date',
		'tag'	 	 	 => esc_html($featured_tag)
	) );
?>

<?php if ( $custom_loop->have_posts() ) : $i = 0; ?>

	<div class="ilovewp-exclusive-post">
		
		<?php while ( $custom_loop->have_posts() ) : $custom_loop->the_post(); $i++; ?>

			<?php $classes = array('ilovewp-post','ilovewp-featured-post','featured-post-simple','featured-post-simple-'.$i); ?>
			<div <?php post_class($classes); ?>>
				<div class="ilovewp-post-wrapper">
					<span class="post-meta-category"><?php esc_html_e( 'Exclusive', 'monograph' ); ?></span>
					<?php the_title( /* translators: post URL */ sprintf( '<h2 class="title-post"><a href="%s" rel="bookmark">', esc_url( get_permalink() ) ), '</a></h2>' ); ?>
					<span class="posted-on"><span class="genericon genericon-time"></span> <time class="entry-date published" datetime="<?php echo get_the_date('c'); ?>"><a href="<?php the_permalink(); ?>" title="<?php the_title_attribute(); ?>"><?php printf( _x( '%s ago', '%s = human-readable time difference', 'monograph' ), human_time_diff( get_the_time( 'U' ), current_time( 'timestamp' ) ) ); ?></a></time></span>
				</div><!-- .ilovewp-post-wrapper -->
			</div><!-- .ilovewp-post .ilovewp-featured-post .featured-post-simple .featured-post-simple-<?php echo $i; ?> -->
		
		<?php endwhile; ?>
		
		<?php wp_reset_postdata(); ?>

	</div><!-- .ilovewp-exclusive-post -->

<?php endif; ?>