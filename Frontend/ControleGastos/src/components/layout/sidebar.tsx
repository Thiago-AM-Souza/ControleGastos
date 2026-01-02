import { NavLink } from 'react-router-dom';
import {
  Users,
  Tags,
  Receipt
} from 'lucide-react';

const menuItems = [
  { to: '/pessoas', icon: Users, label: 'Pessoas' },
  { to: '/categorias', icon: Tags, label: 'Categorias' },
  { to: '/transacoes', icon: Receipt, label: 'Transações' },
];

function Sidebar() {
  return (
    <ul className="nav flex-column nav-pills gap-1 p-3">
      {menuItems.map(item => (
        <li key={item.to} className="nav-item">
          <NavLink to={item.to}
                  end={item.to === '/'}
                  className={({ isActive }) =>
                    `nav-link d-flex align-items-center gap-2 ${
                    isActive ? 'active' : 'text-white'
                  }`
          }>
            <item.icon size={18} />
            {item.label}
          </NavLink>
        </li>
      ))}
    </ul>
  );
}

export default Sidebar;