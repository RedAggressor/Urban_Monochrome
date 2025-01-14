import cl from './header.module.scss';

import { Logo } from '../../shared/ui-kit/Logo/Logo';
import { Container } from '../../shared/Container/Container';
import { Icon, IconType } from '../../shared/ui-kit/Icons/Icons';
import {
  NavLinks,
  NavLinksOrigin,
} from '../../shared/ui-kit/NavLinks/NavLinks';
import { useState } from 'react';

export const Header = () => {
  const [isPopUpOpen, setIsPopUpOpen] = useState(false);
  const [inputValue, setInputValue] = useState('');

  return (
    <Container>
      {' '}
      <header className={cl.header}>
        <div className={cl.header__topContainer}>
          <div className={cl.twoIconsMobileContainer}>
            <Icon type={IconType.Menu} onClick={() => console.log('menu')} />
            <Icon type={IconType.Account} to="/" />
          </div>

          <Logo />

          <div className={cl.twoIconsMobileContainer}>
            <Icon type={IconType.Heart} />
            <Icon type={IconType.Cart} />
          </div>

          {isPopUpOpen ? (
            <form className={cl.popUpForm} onSubmit={e => e.preventDefault()}>
              <Icon type={IconType.Search} onClick={() => {}} />
              <input
                type="text"
                className={cl.popUpForm__input}
                placeholder="Enter the name of the product you want to find"
                value={inputValue}
                onChange={e => setInputValue(e.target.value)}
              />
              <Icon type={IconType.Close} onClick={() => setInputValue('')} />
            </form>
          ) : (
            <NavLinks origin={NavLinksOrigin.Header} className={cl.nav} />
          )}

          <div className={cl.threeIconsTabletContainer}>
            <Icon type={IconType.Heart} />
            <Icon type={IconType.Account} />
            <Icon type={IconType.Cart} />
          </div>

          {isPopUpOpen ? (
            <button
              className={cl.cancelButton}
              onClick={() => setIsPopUpOpen(false)}
            >
              Cancel
            </button>
          ) : (
            <div className={cl.fourIconsDeskContainer}>
              <Icon
                type={IconType.Search}
                onClick={() => setIsPopUpOpen(true)}
              />
              <Icon type={IconType.Heart} />
              <Icon type={IconType.Account} />
              <Icon type={IconType.Cart} />
            </div>
          )}
        </div>

        <form
          className={cl.mobileStaticForm}
          onSubmit={e => e.preventDefault()}
        >
          <input
            className={cl.mobileStaticForm__input}
            type="text"
            placeholder="Search here"
            value={inputValue}
            onChange={e => setInputValue(e.target.value)}
          />
          <Icon
            type={IconType.Search}
            className={cl.mobileStaticForm__icon}
            onClick={() => {}}
          />
        </form>
      </header>
    </Container>
  );
};
