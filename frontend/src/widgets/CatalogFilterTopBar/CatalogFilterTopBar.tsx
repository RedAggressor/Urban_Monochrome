import { Container } from '../../shared/Container/Container';
import cl from './CatalogFilterTopBar.module.scss';

export const CatalogFilterTopBar = () => {
  return (
    <Container className={cl.container}>
      <nav className={cl.filters}>
        <button className={cl.textIconButton}>
          Filters
          <svg
            className={`${cl.textIconButton__icon} ${cl.icon_toggleFilters}`}
          />
        </button>
        <div className={cl.rightButtons}>
          <button className={cl.textIconButton}>
            Sort by
            <svg
              className={`${cl.textIconButton__icon} ${cl.icon_arrowDown}`}
            />
          </button>

          <button className={cl.textIconButton}>
            <svg
              className={`${cl.textIconButton__icon} ${cl.icon_displayList}`}
            />
          </button>
          <button className={cl.textIconButton}>
            <svg
              className={`${cl.textIconButton__icon} ${cl.icon_displayGrid}`}
            />
          </button>
        </div>
      </nav>
    </Container>
  );
};
