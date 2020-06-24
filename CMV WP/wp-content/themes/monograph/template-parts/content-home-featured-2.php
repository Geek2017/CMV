<?php
/**
 * The template used for displaying featured posts on the Front Page.
 *
 * @package Monograph
 */
?>

<?php
	
	$featured_tag = get_theme_mod( 'monograph_featured_term_2', 'featured' );
	
	$custom_loop = new WP_Query( array(
		'post_type'      => 'post',
		'posts_per_page' => 2,
		'order'          => 'DESC',
		'orderby'        => 'date',
		'tag'	 	 	 => esc_html($featured_tag)
	) );
?>

<?php if ( $custom_loop->have_posts() ) : $i = 0; ?>

	<div class="ilovewp-featured-posts ilovewp-featured-posts-secondary">
		
		<ul class="ilovewp-posts clearfix">

		<?php while ( $custom_loop->have_posts() ) : $custom_loop->the_post(); $i++; ?>

			<?php $classes = array('ilovewp-post','ilovewp-featured-post','featured-post-simple','featured-post-simple-'.$i); ?>
			<li <?php post_class($classes); ?>>
				<div class="ilovewp-post-wrapper">
					<?php if ( has_post_thumbnail() ) : ?>
					<div class="post-cover">
						<a href="<?php the_permalink(); ?>" title="<?php the_title_attribute(); ?>"><?php the_post_thumbnail('monograph-large-thumbnail'); ?></a>
					</div><!-- .post-cover -->
					<?php endif; ?>
					<div class="post-preview">
						<div class="post-preview-wrapper">
							<span class="post-meta-category"><?php the_category(esc_html_x( ', ', 'Used on archive and post pages to separate multiple categories.', 'monograph' )); ?></span>
							<?php the_title( /* translators: post URL */ sprintf( '<h2 class="title-post"><a href="%s" rel="bookmark">', esc_url( get_permalink() ) ), '</a></h2>' ); ?>
						</div><!-- .post-preview-wrapper -->
					</div><!-- .post-preview -->
				</div><!-- .ilovewp-post-wrapper -->
			</li><!-- .ilovewp-post .ilovewp-featured-post .featured-post-simple .featured-post-simple-<?php echo $i; ?> -->
		
		<?php endwhile; ?>
		
		<?php wp_reset_postdata(); ?>

		</ul><!-- .ilovewp-posts .clearfix -->
	</div><!-- .ilovewp-featured-posts .ilovewp-featured-posts-secondary -->

<?php endif; ?>