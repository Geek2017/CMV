<?php
/**
 * The main template file.
 *
 * This is the most generic template file in a WordPress theme
 * and one of the two required files for a theme (the other being style.css).
 * It is used to display a page when nothing more specific matches a query.
 * E.g., it puts together the home page when no home.php file exists.
 *
 * @link https://codex.wordpress.org/Template_Hierarchy
 *
 * @package Monograph
 */

get_header(); ?>

	<div id="site-main" class="content-home">

		<div class="wrapper wrapper-main clearfix">
		
			<?php if ( is_home() && !is_paged() ) { ?>
			
				<?php
				if ( 1 == get_theme_mod( 'monograph_front_featured_posts', 1 ) ) {
					get_template_part( 'template-parts/content', 'home-featured' );
				}
				if ( 1 == get_theme_mod( 'monograph_front_featured_posts_2', 1 ) ) {
					get_template_part( 'template-parts/content', 'home-featured-2' );
				}
				?>

			<?php } ?>
			
			<main id="site-content" class="site-main" role="main">
			
				<div class="site-content-wrapper clearfix">

					<?php if ( have_posts() ) : $i = 0; ?>
					
					<?php if ( is_home() && ! is_front_page() ) : ?>
						<header>
							<h1 class="page-title screen-reader-text"><?php single_post_title(); ?></h1>
						</header>
					<?php endif; ?>

					<?php if ( is_home() && !is_paged() ) { ?>
					
						<?php
						if ( 1 == get_theme_mod( 'monograph_front_featured_categories', 0 ) ) {
							get_template_part( 'template-parts/content', 'home-categories' );
						}
						
						?>

					<?php } ?>
					
					<?php if ( is_home() && !is_paged() ) { ?><p class="widget-title"><?php _e('Recent Posts','monograph'); ?></p><?php } ?>
					
					<ul id="recent-posts" class="ilovewp-posts ilovewp-posts-archive clearfix">
						
						<?php while (have_posts()) : the_post(); ?>
						
						<?php get_template_part( 'template-parts/content'); ?>
						
						<?php endwhile; ?>
						
					</ul><!-- .ilovewp-posts .ilovewp-posts-archive -->

					<?php 
					$args['prev_text'] = '<span class="nav-link-label"><span class="genericon genericon-previous"></span></span>' . __('Older Posts', 'monograph');
					$args['next_text'] = __('Newer Posts', 'monograph') . '<span class="nav-link-label"><span class="genericon genericon-next"></span></span>';
					the_posts_navigation($args); ?>
			
					<?php else : ?>
			
						<?php get_template_part( 'template-parts/content', 'none' ); ?>
			
					<?php endif; ?>

				</div><!-- .site-content-wrapper .clearfix -->
			
			</main><!-- #site-content -->
			
			<?php get_sidebar(); ?>
		
		</div><!-- .wrapper .wrapper-main -->

	</div><!-- #site-main -->

<?php get_footer(); ?>