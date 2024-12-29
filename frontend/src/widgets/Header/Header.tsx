import cl from './header.module.scss';

import { Logo } from '../../shared/ui-kit/Logo/Logo';
import { Container } from '../../shared/Container/Container';
import { Icon, IconType } from '../../shared/ui-kit/Icon/Icon';
import { useAppSelector } from '../../shared/hooks';

export const Header = () => {
  return (
    <Container>
      {' '}
      <header className={cl.header}>
        <div className={cl.menuAccountContainer}>
          <Icon type={IconType.Account} to="/" />
        </div>
        <Logo />
      </header>
    </Container>
  );
};
